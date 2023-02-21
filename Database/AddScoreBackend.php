<?php //AddScoreBackEnd.php
// Connect database 
include("dbconninc.php");
//check if POST fields are received, else quit
if(!isset($_POST["sPlayerName"]) || !isset($_POST["iScore"]))die("not posted!");
$sPlayerName=$_POST["sPlayerName"];
$sScore=$_POST["iScore"];
echo "Recieved: $sPlayerName: $sScore"; //to show what has been recieved
//Prepare statement..? denotes to link to php variables later
$query="insert into tb_leaderboard (username, score) values (?,?)";
$stmt=$conn->prepare($query);
//s - string, i - integer...make sure it matches the data types!
$stmt->bind_param("si", $sPlayerName, $sScore);
//Execute statement
$stmt->execute();
echo "<p>Num rows added:$stmt->affected_rows";
$stmt->close(); //Close statement
?>
