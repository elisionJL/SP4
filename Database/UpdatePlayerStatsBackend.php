<?php //ReadScoreboard.php
// Connect database
include("dbconninc.php");
// Prepare Statement (SQL query)
if(!isset($_POST["username"]) || !isset($_POST["Hostage"]) || !isset($_POST["LevelCleared"]) || !isset($_POST["TimesPlayed"]))die("not posted!");

$sPlayerName=$_POST["username"];
$iHostages=$_POST["Hostage"];
$iLvlCleared=$_POST["LevelCleared"];
$iTimesPlayed=$_POST["TimesPlayed"];

$query="select Username from tb_playerstats where Username=?";
$stmt=$conn->prepare($query);
$stmt->bind_param("s", $sPlayerName);
$stmt->execute();
$stmt->store_result();
$row=$stmt->affected_rows;
$stmt->bind_result($sPlayerName);
$stmt->fetch();

if($row==0){
    //Prepare statement..? denotes to link to php variables later
    $query2="insert into tb_playerstats (Username, HostagesLeft, LvlCleared, TimePlayed) values (?,?,?,?)";
    $stmt=$conn->prepare($query2);
    //s - string, i - integer...make sure it matches the data types!
    $stmt->bind_param("siii", $sPlayerName, $iHostages, $iLvlCleared, $iTimesPlayed);
    //Execute statement
    $stmt->execute();
    echo "<p>Num rows added:$stmt->affected_rows";
}
else{
    //Prepare statement..? denotes to link to php variables later
    $query3 ="update tb_playerstats set HostagesLeft=?, LvlCleared=?, TimePlayed=? where username=?";
    $stmt=$conn->prepare($query3);
    //s - string, i - integer...make sure it matches the data types!
    $stmt->bind_param("iiis", $iHostages, $iLvlCleared, $iTimesPlayed, $sPlayerName);
    //Execute statement
    $stmt->execute();
}
$stmt->close();
$conn->close();
?>


