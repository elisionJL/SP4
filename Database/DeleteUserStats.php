<?php //ReadScoreboard.php
// Connect database
include("dbconninc.php");
// Prepare Statement (SQL query)
if(!isset($_POST["username"]))die("not posted!");
$sUsername=$_POST["username"];
//Prepare statement..? denotes to link to php variables later
$query="DELETE FROM tb_playerstats WHERE username=?";
$stmt=$conn->prepare($query);
//s - string, i - integer...make sure it matches the data types!
$stmt->bind_param('s', $sUsername);
//Execute statement
$stmt->execute();
echo "<p>num row(s) affected:$stmt->affected_rows";
$stmt->close(); //Close statement
?>

