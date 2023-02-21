<?php //ReadScoreboard.php
// Connect database
include("dbconninc.php");
// Prepare Statement (SQL query)
if(!isset($_POST["username"]))die("not posted!");
$sPlayerName=$_POST["username"];
$Tower1Get=0;
$Tower2Get=0;
$Tower3Get=0;
$Tower4Get=0;
$Tower5Get=0;

$query="select Username from tb_towerselect where Username=?";
$stmt=$conn->prepare($query);
$stmt->bind_param("s", $sPlayerName);
$stmt->execute();
$stmt->store_result();
$row=$stmt->num_rows();
$stmt->bind_result($sPlayerName);
$stmt->fetch();
if($row==0){

}
else{
    $query2="select Username,Tower1,Tower2,Tower3,Tower4,Tower5 from tb_towerselect where Username=?";
    $stmt=$conn->prepare($query2);
    //s - string, i - integer...make sure it matches the data types!
    $stmt->bind_param("s", $sPlayerName);
    // Execute Statement
    $stmt->execute();
    //Link results to variables
    $stmt->bind_result($sPlayerName, $Tower1Get, $Tower2Get, $Tower3Get, $Tower4Get, $Tower5Get);   
    $arr=Array(); //JSON use: create main array
    //Fetch results
    while($stmt->fetch()){
    //JSON use: create associative array for each record //4json
    $TowerArray=array("username"=>$sPlayerName,"Tower1"=>$Tower1Get,"Tower2"=>$Tower2Get,"Tower3"=>$Tower3Get,"Tower4"=>$Tower4Get,"Tower5"=>$Tower5Get);
    //JSON use: add to main index array
    array_push($arr,$TowerArray); //corrected typo
    }
    $stmt->close(); // Close Statement
    $conn->close(); // Close connection
    http_response_code(200); //JSON use: tell the client everything ok //4json
    echo json_encode($arr[0]); //JSON use: encode the json format
}
?>