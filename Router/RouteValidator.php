<?php
class VarContainer {
    public $name="";
    public $value="";
    public $regex="";
    public $result=FALSE;

    function __construct($name, $value, $regex){
        $this->name = $name;
        $this->value = $value;
        $this->regex = $regex;
    }
}

class APIRoute {


    private $URIBaseline = ""; // 'regexTester.php/{username:[A-Za-z0-9]+}'
    private $URIToValidate = ""; // regexTester.php/AZ4F4
    public $vars = array();
    public $routeIsValid = FALSE;


    /*function __construct($inURIBaseline, $inURIToValidate){
        $this->URIBaseline = $inURIBaseline;
        $this->URIToValidate = $inURIToValidate;
    }*/

    function validateRoute($inURIBaseline, $inURIToValidate){
        
        $this->URIBaseline = $inURIBaseline;
        $this->URIToValidate = $inURIToValidate;
        
        try{
                $this->URIToValidate = trim($this->URIToValidate,'/').'/';
                preg_match_all('#\{(.*?)\}#', $this->URIBaseline, $out_matchesPattern);
                preg_match_all('#\/(.*?)\/#', $this->URIToValidate, $out_matchesRequest);

                #are there any variables?
                if(count($out_matchesPattern[1]) === 0){
                    if(trim($this->URIBaseline,'/') === trim($this->URIToValidate,'/')){
                        $this->routeIsValid = TRUE;
                        return;
                    }
                }

                //var_dump($out_matchesPattern);
                //var_dump($out_matchesRequest);
                
                if(count($out_matchesPattern[1]) > 1){
                    for($i = 0; $i < ( count($out_matchesPattern[1])) ; $i++){
                        $varNameContainer = explode(":", $out_matchesPattern[1][$i]);
                        $varContainer = new VarContainer($varNameContainer[0], $out_matchesRequest[1][$i], $varNameContainer[1]);
                        $varContainer->result = $this->validateInputWithBaseline($varContainer->regex, $varContainer->value);
                        array_push($this->vars, $varContainer);
                    }
                }
                else{
                    for($i = 0; $i < ( count($out_matchesPattern[1])) ; $i++){
                        $varNameContainer = explode(":", $out_matchesPattern[1][$i]);
                        $varContainer = new VarContainer($varNameContainer[0], $out_matchesRequest[1][$i], $varNameContainer[1]);
                        $varContainer->result = $this->validateInputWithBaseline($varContainer->regex, $varContainer->value);
                        array_push($this->vars, $varContainer);
                    }
                }
                foreach($this->vars as $var){
                    if(!$var->result){
                        #one of the variables does not match the format, so request is not valid.
                        $this->routeIsValid = FALSE;
                        return;
                    }
                    
                    $this->routeIsValid = TRUE;
                    return;
                }
        }
        catch (Exception $e) {
            #do nothing, keep going.
            $this->routeIsValid = FALSE;
            return;
        }
    }

    function validateInputWithBaseline($pattern, $input){
    
        if(!isset($input) || empty($input)){
            return FALSE;
        }
        if(preg_match('#'.$pattern.'#', $input, $matches) === false){
            return FALSE; 
        }else{
            if($matches[0] === $input){ //input fully match ?
                return TRUE; 
            }
            else{
                return FALSE; 
            }
        }

    }

}

?>