<?php
include("dbconninc.php");
if(!isset($_POST["Email"]) || !isset($_POST["Password"]))die("not posted!");
$sEmail=$_POST["Email"];
$sPassword=$_POST["Password"];
//echo "Recieved Login: $sUsername: $sPassword"; //to show what is recieved
$query="update tb_users set password=? where Email=?";
$stmt=$conn->prepare($query);
$stmt->bind_param("ss", $sPassword, $sEmail);
$stmt->execute();
$stmt->close();
$conn->close();
echo "Password changed";
?>