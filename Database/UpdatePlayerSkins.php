<?php //ReadScoreboard.php
// Connect database
include("dbconninc.php");
// Prepare Statement (SQL query)
if(!isset($_POST["username"]) || !isset($_POST["Skin"]))die("not posted!");

$sPlayerName=$_POST["username"];
$sSkin=$_POST["Skin"];

$query="select Username from tb_OwnedSkins where Username=?";
$stmt=$conn->prepare($query);
$stmt->bind_param("s", $sPlayerName);
$stmt->execute();
$stmt->store_result();
$row=$stmt->num_rows();
$stmt->bind_result($sPlayerName);
$stmt->fetch();

if($row==0){
    //Prepare statement..? denotes to link to php variables later
    $query2="insert into tb_OwnedSkins (Username, PlayerSkins) values (?,?)";
    $stmt=$conn->prepare($query2);
    //s - string, i - integer...make sure it matches the data types!
    $stmt->bind_param("ss", $sPlayerName, $sSkin);
    //Execute statement
    $stmt->execute();
    echo "<p>Num rows added:$stmt->affected_rows";
}
else{
    //Prepare statement..? denotes to link to php variables later
    $query3 ="update tb_OwnedSkins set PlayerSkins = ? where username=?";
    $stmt=$conn->prepare($query3);
    //s - string, i - integer...make sure it matches the data types!
    $stmt->bind_param("ss", $sSkin, $sPlayerName);
    //Execute statement
    $stmt->execute();
}
$stmt->close();
$conn->close();
?>


