<?php
// Connect database //AddScore.php
include('dbconninc.php');
//check if POST fields are received, else quit
if(!isset($_POST["sUsername"]) || !isset($_POST["sPassword"]) || !isset($_POST["sEmail"]))die("not posted!");
$sUsername=$_POST["sUsername"];
$sPassword=$_POST["sPassword"];
$sEmail=$_POST["sEmail"];
echo "Recieved: $sUsername: $sPassword:$sEmail"; //to show what has been recieved
//Prepare statement..? denotes to link to php variables later
$details="select uid from tb_users where Username=? and Password=?";
$stmt=$conn->prepare($details);
$stmt->bind_param("ss", $sUsername, $sPassword);
$stmt->execute();
$stmt->store_result();
$row=$stmt->num_rows();
$stmt->bind_result($uid);
$stmt->fetch();
if($row==0){
    $query="insert into tb_users (username, password, email) values (?,?,?)";
    $stmt=$conn->prepare($query);
    //s - string, i - integer...make sure it matches the data types!
    $stmt->bind_param("sss", $sUsername, $sPassword, $sEmail);
    //Execute statement
    $stmt->execute();
    echo "<p>Num rows added:$stmt->affected_rows";
}
else{
    // some data matched
    echo "<p>Someone already registered using one or more of these details";
}
$stmt->close();
$conn->close();
?>
