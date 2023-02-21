<?php //ReadScoreboard.php
// Connect database
include("dbconninc.php");
// Prepare Statement (SQL query)
if(!isset($_POST["username"]))die("not posted!");
$sPlayerName=$_POST["username"];
$Music_1=0;
$Music_2=0;
$Music_3=0;
$Music_4=0;
$Music_5=0;

$query="select Username from tb_music where Username=?";
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
    $query2="select Username,Music_1,Music_2,Music_3,Music_4,Music_5 from tb_music where Username=?";
    $stmt=$conn->prepare($query2);
    //s - string, i - integer...make sure it matches the data types!
    $stmt->bind_param("s", $sPlayerName);
    // Execute Statement
    $stmt->execute();
    //Link results to variables
    $stmt->bind_result($sPlayerName, $Music_1, $Music_2, $Music_3, $Music_4, $Music_5);   
    $arr=Array(); //JSON use: create main array
    //Fetch results
    while($stmt->fetch()){
    //JSON use: create associative array for each record //4json
    $musicArray=array("username"=>$sPlayerName,"Music_1"=>$Music_1,"Music_2"=>$Music_2,"Music_3"=>$Music_3,"Music_4"=>$Music_4,"Music_5"=>$Music_5);
    //JSON use: add to main index array
    array_push($arr,$musicArray); //corrected typo
    }
    $stmt->close(); // Close Statement
    $conn->close(); // Close connection
    http_response_code(200); //JSON use: tell the client everything ok //4json
    echo json_encode($arr[0]); //JSON use: encode the json format
}
?>