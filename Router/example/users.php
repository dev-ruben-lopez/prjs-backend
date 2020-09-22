<?php
/*
ENDPOINTS OF THIS CLASS

return all records
GET /user

return a specific record
GET /user/{username}

create a new record
POST /user

update an existing record
PUT /puser/{username}

delete an existing record
DELETE /user/{username}


REQUEST COMPOSITION:
username or userid of the requestor needs to be included in the URL.

*/

include '../../init.php';
include_once ROOT_DIR.'/include/class-autoload.inc.php';
include_once ROOT_DIR.'/classes/Router/RouteValidator.php';
include_once ROOT_DIR.'/classes/Router/RouteCollector.php';
include_once ROOT_DIR.'/classes/Router/Dispatcher.php';
include_once 'usersGateaway.php';
#FastRoute
/*require_once (ROOT_DIR.'/include/FastRoute/src/RouteCollector.php');

require_once (ROOT_DIR.'/include/FastRoute/src/RouteParser.php');
require_once (ROOT_DIR.'/include/FastRoute/src/RouteParser/Std.php');

require_once (ROOT_DIR.'/include/FastRoute/src/Route.php');
require_once (ROOT_DIR.'/include/FastRoute/src/BadRouteException.php');

require_once (ROOT_DIR.'/include/FastRoute/src/DataGenerator.php');
require_once (ROOT_DIR.'/include/FastRoute/src/DataGenerator/RegexBasedAbstract.php');
require_once (ROOT_DIR.'/include/FastRoute/src/DataGenerator/CharCountBased.php');
require_once (ROOT_DIR.'/include/FastRoute/src/DataGenerator/GroupCountBased.php');
require_once (ROOT_DIR.'/include/FastRoute/src/DataGenerator/GroupPosBased.php');
require_once (ROOT_DIR.'/include/FastRoute/src/DataGenerator/MarkBased.php');

require_once (ROOT_DIR.'/include/FastRoute/src/Dispatcher.php');
require_once (ROOT_DIR.'/include/FastRoute/src/Dispatcher/RegexBasedAbstract.php');
require_once (ROOT_DIR.'/include/FastRoute/src/Dispatcher/CharCountBased.php');
require_once (ROOT_DIR.'/include/FastRoute/src/Dispatcher/GroupCountBased.php');
require_once (ROOT_DIR.'/include/FastRoute/src/Dispatcher/GroupPosBased.php');
require_once (ROOT_DIR.'/include/FastRoute/src/Dispatcher/MarkBased.php');

require_once (ROOT_DIR.'/include/FastRoute/src/functions.php');
*/


header("Access-Control-Allow-Origin: *"); //todo : Define the permissions level if they are allowed to read,create,mod and delete users.
header("Content-Type: application/json; charset=UTF-8");
header("Access-Control-Allow-Methods: OPTIONS,GET,POST,PUT,DELETE");
header("Access-Control-Max-Age: 3600");
header("Access-Control-Allow-Headers: Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");

#add valid routes
$routeCollector = array(
    'getUsers' => new Route('GET', 'users.php', 'get_all_users_handler'),
    'getUser' => new Route('GET', 'users.php/{username:[A-Za-z0-9]+}', 'get_user_handler'),
    'postUser' => new Route('POST', 'users.php', 'post_user_handler'),
    'putUser'=> new Route('PUT', 'users.php/{username:[A-Za-z0-9]+}', 'put_user_handler'),
    'deleteUser' => new Route('DELETE', 'users.php/{username:[A-Za-z0-9]+}', 'delete_user_handler')
);


// Fetch method and URI 
$httpMethod = $_SERVER['REQUEST_METHOD'];
$uri = $_SERVER['REQUEST_URI'];
$URIfinalSegment = str_replace(ROOT_URL.'/api/v1/', "", $_SERVER['PHP_SELF']);

// Strip query string (?foo=bar) and decode URI
if (false !== $pos = strpos($uri, '?')) {
    $uri = substr($uri, 0, $pos);
}
$uri = rawurldecode($uri);

/*
HERE, pass the Route Collector to the Validator and check if one route is valid.
*/
$dispatcher = new Dispatcher($routeCollector, $URIfinalSegment, $httpMethod);
$routeInfo = $dispatcher->dispatch();
// RouteInfo = array()
//


/*$response['status_code_header'] = 'HTTP/1.1 200 OK';
var_dump($routeInfo);
return;
*/

$processRequest = new UsersGateway("", "", "");
switch ($routeInfo['Result']) {
    case $dispatcher::NOT_FOUND:
        $response = $processRequest->notFoundResponse();
        break;
    case $dispatcher::METHOD_NOT_ALLOWED:
        //$allowedMethods = $routeInfo[1];
        $response = $processRequest->unprocessableEntityResponse($dispatcher->vars);
        break;
    case $dispatcher::FOUND:
        $handler = $routeInfo['handler'];
        $vars = $routeInfo['vars'];
        $processRequest = new UsersGateway($httpMethod, $handler, $vars);
        $response = $processRequest->processRequest();
        break;
}

echo $response;

?>
