<?php //ReadScoreboard.php
// Connect database
include("dbconninc.php");
// Prepare Statement (SQL query)
if(!isset($_POST["sUsername"]))die("not posted!");
$sUsername=$_POST["sUsername"];
echo "Recieved: $sUsername"; //to show what has been recieved
//Prepare statement..? denotes to link to php variables later
$query="DELETE FROM tb_users WHERE username=?";
$stmt=$conn->prepare($query);
//s - string, i - integer...make sure it matches the data types!
$stmt->bind_param('s', $sUsername);
//Execute statement
$stmt->execute();
echo "<p>num row(s) affected:$stmt->affected_rows";
$stmt->close(); //Close statement
?>

