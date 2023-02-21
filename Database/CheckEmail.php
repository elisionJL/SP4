<?php
include("dbconninc.php");
if(!isset($_POST["Email"]))die("not posted!");
$sEmail=$_POST["Email"];
//echo "Recieved Login: $sUsername: $sPassword"; //to show what is recieved
$query="select uid from tb_users where Email=?";
$stmt=$conn->prepare($query);
$stmt->bind_param("s", $sEmail);
$stmt->execute();
$stmt->store_result();
$row=$stmt->num_rows();
$stmt->bind_result($uid);
$stmt->fetch();
$stmt->close();
$conn->close();
if($row==0){
    echo "Email does not exist";
}
else{
    echo "Email Found";
}
?>