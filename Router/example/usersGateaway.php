<?php
include '../../init.php';
include_once ROOT_DIR.'/include/class-autoload.inc.php';


class UsersGateway {
    
    public $vars, $httpMethod, $handler;
    public $dbObject;
    public $utils;

    public function __construct($httpMethod, $handler, $vars){

        $this->vars = $vars;
        $this->httpMethod = $httpMethod;
        $this->handler = $handler;
        $this->dbObject = new DB();
        $this->utils = new Utils();
    }

    public function processRequest()
    {
        switch ($this->handler) {
            case 'get_all_users_handler':
                $response = $this->getAllUsersData();
                break;
            case 'get_user_handler':
                $response = $this->getUserData();
                break;
            case 'post_user_handler':
                $response = $this->postUserData();
                break;
            case 'put_user_handler':
                $response = $this->putUserData();
                break;
            case 'delete_user_handler':
                $response = $this->deleteUserData();
                break;
            default:
                $response = $this->notFoundResponse();
                break;
        }
        header($response['status_code_header']);
        if ($response['body']) {
            return $response['body'];
        }
    }

    public function getAllUsersData()
    {
        $sql = "SELECT * FROM dbo.users";
        $result = $this->dbObject->queryDB($sql);
        if (! $result) {
            return $this->notFoundResponse();
        }
        $response['status_code_header'] = 'HTTP/1.1 200 OK';
        $response['body'] = json_encode($result);
        return $response;
    }

    function getUserData()
    {
        //TODO I know that $this->vars[0]->value contains the username value, but find a way to make this more elegant
        $paramsArray = array($this->vars[0]->value);
        $sql = "SELECT * FROM dbo.users where username = ?";
        $result = $this->dbObject->queryDBwithParams($sql,$paramsArray);
        if (! $result) {
            return $this->notFoundResponse();
        }
        $response['status_code_header'] = 'HTTP/1.1 200 OK';
        $response['body'] = json_encode($result);
        return $response;
    }

    function postUserData()
    {
        $input = (array) json_decode(file_get_contents('php://input'), TRUE);

        if (! $this->validateUser($input)) {
            return $this->unprocessableEntityResponse("Incorrect or missing input data.");
        }

        $paramsArray = array($input["username"]);
        $sql = "SELECT * FROM dbo.users where username = ?";
        $result = $this->dbObject->queryDBwithParams($sql,$paramsArray);

        if ($result) {
            return $this->unprocessableEntityResponse("User already exists.");
        }
        
        $query = "INSERT INTO DBO.USERS(username,firstname,lastname,permissions_level,email) VALUES(?,?,?,?,?);";

        /*Dealing with associative arrays error :( */
        $inputUser = array($input["username"],$input["firstname"],$input["lastname"],$input["permissions_level"],$input["email"]);

        $insertResult = $this->dbObject->createDBwithParams($query, $inputUser);

        if($insertResult === TRUE){
            $response['status_code_header'] = 'HTTP/1.1 201 Created';
            $response['body'] = null;
        }
        else{
            return $this->unprocessableEntityResponse("Error while trying to create User. " . json_encode($insertResult));
        }
             
        return $response;
    }

    function putUserData()
    {
        $input = (array) json_decode(file_get_contents('php://input'), TRUE);
        $paramsArray = array($this->vars[0]['username']);
        $sql = "SELECT * FROM dbo.users where username = ?";
        $result = $this->dbObject->queryDBwithParams($sql,$paramsArray);
        if (!$result) {
           return $this->notFoundResponse();
        }

        $query = "UPDATE DBO.USERS SET ";
        if(!$this->utils->StringIsNullOrEmpty($input["firstname"])){
            $query .= " firstname =  '" . $input["firstname"] . "' , ";
        }
        if(!$this->utils->StringIsNullOrEmpty($input["lastname"])){
            $query .= " lastname =  '" . $input["lastname"] . "' ,";
        }
        if(!$this->utils->StringIsNullOrEmpty($input["permissions_level"])){
            $query .= " permissions_level =  " . $input["permissions_level"] . " ,";
        }
        if(!$this->utils->StringIsNullOrEmpty($input["email"])){
            $query .= " email =  '" . $input["email"] . "' ,";
        }

        $query = rtrim($query, ',');
        $query .= " WHERE username = '".$input["username"]."'; ";
        
        $updateResult = $this->dbObject->updateDB($query);

        if($updateResult === TRUE){
            $response['status_code_header'] = 'HTTP/1.1 200 OK';
            $response['body'] = null;
        }
        else{
            return $this->unprocessableEntityResponse("Error while trying to update User. " . json_encode($insertResult));
        }
             
        return $response;
    }

    function deleteUserData($id)
    {   
        $paramsArray = array($this->vars[0]['username']);
        $sql = "DELETE FROM dbo.users where username = ?";
        $result = $this->dbObject->queryDBwithParams($sql,$paramsArray);
        if (! $result) {
            return $this->notFoundResponse();
        }
        $response['status_code_header'] = 'HTTP/1.1 204 No Content';
        $response['body'] = json_encode($result);
        return $response;
    }

    function validateUser($input)
    {
       
        if ($this->utils->StringIsNullOrEmpty($input['firstname'])) {
            return false;
        }
        if ($this->utils->StringIsNullOrEmpty($input['lastname'])) {
            return false;
        }
        if ($this->utils->StringIsNullOrEmpty($input['username'])) {
            return false;
        }
        if ($this->utils->StringIsNullOrEmpty($input['permissions_level'])) {
            return false;
        }
        if ($this->utils->StringIsNullOrEmpty($input['email'])) {
            return false;
        }

        return true;
    }

    function unprocessableEntityResponse($errorMessage = 'Invalid Input')
    {
        $response['status_code_header'] = 'HTTP/1.1 422 Unprocessable Entity';
        $response['body'] = json_encode([
            'error' => $errorMessage
        ]);
        return $response;
    }

    function notFoundResponse()
    {
        $response['status_code_header'] = 'HTTP/1.1 404 Not Found';
        $response['body'] = null;
        return $response;
    }

}


?>