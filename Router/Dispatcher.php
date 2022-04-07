<?php
include_once 'RouteCollector.php';
include_once 'RouteValidator.php';

class Dispatcher{

    /*
    If NOT_FOUND, then return just array('Result' => NOT_FOUND )
    If METHOD_NOT_ALLOWED, then return just array('Result' => METHOD_NOT_ALLOWED )
    If FOUND, then return  array('Result' => FOUND, 'handler' => Handler From arrayOfRoutes, 'vars' => vars and values )
    */

    public const NOT_FOUND = 0;
    public const FOUND = 1;
    public const METHOD_NOT_ALLOWED = 2;

    public $arrayOfValidRoutes, $routeRequested, $httpRequestMethod;


    function __construct($arrayOfValidRoutes, $routeRequested, $httpRequestMethod){
        $this->arrayOfValidRoutes = $arrayOfValidRoutes;
        $this->routeRequested = $routeRequested;
        $this->httpRequestMethod = $httpRequestMethod;
    }


    function dispatch(){
        try{
            #simply validations
            if( $this->NullOrEmpty($this->arrayOfValidRoutes) || $this->NullOrEmpty($this->routeRequested) ) {
                return array('result' => $this::NOT_FOUND);
            }

            #search for valid route
            $apiRouteValidator = new APIRoute();
            foreach($this->arrayOfValidRoutes as $validRoute){
                $apiRouteValidator->validateRoute($validRoute->route, $this->routeRequested);
                #if the request URI match one of the valid routes
                if($apiRouteValidator->routeIsValid && ($this->httpRequestMethod === $validRoute->httpMethod)){
                    return array(
                        'Result' => $this::FOUND, 
                        'handler' => $validRoute->handler,
                        'vars' => $apiRouteValidator->vars
                    );
                }            
            }
            #if no match
            return array(
                'Result' => $this::NOT_FOUND, 
                'handler' => null,
                'vars' => null
            );
        }
        catch (Exception $e) {
            return array(
                'Result' => $this::METHOD_NOT_ALLOWED, 
                'handler' => null,
                'vars' => $e //this is only for log messages or debug
            );
        }

    }


    private function NullOrEmpty($var){

        if(!isset($var) || empty($var)){ return TRUE;}
        return FALSE;
        
    }

}

?>