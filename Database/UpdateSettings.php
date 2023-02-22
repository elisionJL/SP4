<?php //UpdateSettings.php
// Connect database
include("dbconninc.php");
if (!isset($_POST["username"]) || !isset($_POST["newbgm"]) 
    || !isset($_POST["newbgm"])|| !isset($_POST["newmaster"]))
    die("not posted!");
$sUsername = $_POST["username"];
$sNewBGM = $_POST["newbgm"];
$sNewSFX = $_POST["newsfx"];
$sNewMaster = $_POST["newmaster"];

//$query = "insert into tb_users (username, password, email) values (?,?,?);";
$query = "select * from tb_settings where Username=?";
$stmt = $conn->prepare($query);
$stmt->bind_param("s", $sUsername);
$stmt->execute();
$stmt->store_result();
//echo "<p>Num rows added:$stmt->affected_rows";
$rows = $stmt->affected_rows;


if($rows == 0){
    $query = "insert into tb_settings (Username, MasterVolume, SFXVolume,BGMVolume) values (?,?,?,?);";
    $stmt = $conn->prepare($query);
    $stmt->bind_param("siii", $sUsername,$sNewMaster,$sNewSFX,$sNewBGM);
    $stmt->execute();
}
else{
    $query = "Update tb_settings set MasterVolume=?,SFXVolume=?,BGMVolume=? where Username=?;";
    $stmt = $conn->prepare($query);
    $stmt->bind_param("iiis",$sNewMaster,$sNewSFX,$sNewBGM, $sUsername);
    $stmt->execute();
}
$rows = $stmt->affected_rows;
if($rows == 0){
    echo "server error";
}
else{
    echo "Player settings saved";
}

$stmt->close();
$conn->close();
?>