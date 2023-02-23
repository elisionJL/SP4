<?php
include("dbconninc.php");
if(!isset($_POST["sUsername"]) || !isset($_POST["sPassword"]))die("not posted!");
$sUsername=$_POST["sUsername"];
$sPassword=$_POST["sPassword"];
//echo "Recieved Login: $sUsername: $sPassword"; //to show what is recieved
$query="select username from tb_playerstats where Username=? and Password=?";
$stmt=$conn->prepare($query);
$stmt->bind_param("ss", $sUsername, $sPassword);
$stmt->execute();
$stmt->store_result();
$row=$stmt->num_rows();
$stmt->bind_result($uid);
$stmt->fetch();

if($row==0){
    
    echo "Login Failure";
}
else{
    date_default_timezone_set('Asia/Singapore');
    $date = date('Y-m-d H:i:s');
    
    $query2="Update tb_playerstats set LastLogin = ? where Username = ?";
    $stmt=$conn->prepare($query2);
    //s - string, i - integer...make sure it matches the data types!
    $stmt->bind_param("ss", $date, $sUsername);
    $stmt->execute();
    //Execute statement
    echo "Login Success";
}
$stmt->close();
$conn->close();
?>