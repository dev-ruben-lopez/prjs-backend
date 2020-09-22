<?PHP

class Route{
    public $method;
    public $route;
    public $handler;

    function __construct($method,$route,$handler){

        $this->httpMethod = $method;
        $this->route = $route;
        $this->handler = $handler;

    }

}


?>