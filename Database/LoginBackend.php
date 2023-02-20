<?php
include("dbconninc.php");
if(!isset($_POST["sUsername"]) || !isset($_POST["sPassword"]))die("not posted!");
$sUsername=$_POST["sUsername"];
$sPassword=$_POST["sPassword"];
//echo "Recieved Login: $sUsername: $sPassword"; //to show what is recieved
$query="select uid from tb_users where Username=? and Password=?";
$stmt=$conn->prepare($query);
$stmt->bind_param("ss", $sUsername, $sPassword);
$stmt->execute();
$stmt->store_result();
$row=$stmt->num_rows();
$stmt->bind_result($uid);
$stmt->fetch();
$stmt->close();
$conn->close();
if($row==0){
    echo "Login Failure";
}
else{
    echo "Login Success";
}
?>