<?php //ReadScoreboard.php
// Connect database
include("dbconninc.php");
// Prepare Statement (SQL query)
if(!isset($_POST["username"]))die("not posted!");
$sPlayerName=$_POST["username"];

$query="select Username from tb_OwnedSkins where Username=?";
$stmt=$conn->prepare($query);
$stmt->bind_param("s", $sPlayerName);
$stmt->execute();
$stmt->store_result();
$row=$stmt->num_rows();
$stmt->bind_result($sPlayerName);
$stmt->fetch();
if($row==0){
    echo "Cannot Find";
}
else{
    $query2="select PlayerSkins from tb_OwnedSkins where Username=?";
    $stmt=$conn->prepare($query2);
    //s - string, i - integer...make sure it matches the data types!
    $stmt->bind_param("s", $sPlayerName);
    // Execute Statement
    $stmt->execute();
    //Link results to variables
    $stmt->bind_result($sPlayerSkins);
    $stmt->fetch();
    echo $sPlayerSkins;
}
$stmt->close(); // Close Statement
$conn->close();
?>