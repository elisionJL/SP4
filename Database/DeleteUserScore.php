<?php //DeleteAllScores.php
//check if POST fields are received, else quit

// Connect database 
include("dbconninc.php");
if(!isset($_POST["username"]))die("not posted!");
$sPlayerName=$_POST["username"];
$query="delete from tb_leaderboard where username=?";
$stmt=$conn->prepare($query);
$stmt->bind_param('s', $sPlayerName);
// Execute Statement
$stmt->execute();
echo "<p>num row(s) affected:$stmt->affected_rows";
$stmt->close(); // Close Statement
?>