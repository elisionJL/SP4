<?php //ReadScoreboard.php
// Connect database
include("dbconninc.php");
// Prepare Statement (SQL query)
if(!isset($_POST["username"]) || !isset($_POST["newlevel"]) || !isset($_POST["newxp"]) || !isset($_POST["newcash"])
|| !isset($_POST["newtimesplayed"])|| !isset($_POST["CanJump"])|| !isset($_POST["TimeUP"])|| !isset($_POST["CustomColour"])||
!isset($_POST["MusicChosen"]))die("not posted!");

$sPlayerName=$_POST["username"];
$iLevel=$_POST["newlevel"];
$iXP=$_POST["newxp"];
$iCash=$_POST["newcash"];
$iTimesPlayed=$_POST["newtimesplayed"];
$iJumpUpgrade=$_POST["CanJump"];
$iTimeUpgrade=$_POST["TimeUP"];
$ChangeColour=$_POST["CustomColour"];
$MusicPlaying=$_POST["MusicChosen"];

$query="select Username from tb_playerstats where Username=?";
$stmt=$conn->prepare($query);
$stmt->bind_param("s", $sPlayerName);
$stmt->execute();
$stmt->store_result();
$row=$stmt->num_rows();
$stmt->bind_result($sPlayerName);
$stmt->fetch();

if($row==0){
    //Prepare statement..? denotes to link to php variables later
    $query2="insert into tb_playerstats (Username, Level, XP, cash, timesplayed, jumpUpgrade, timeUpgrade, ChangeColour, CurrentlyPlaying) values (?,?,?,?,?,?,?,?,?)";
    $stmt=$conn->prepare($query2);
    //s - string, i - integer...make sure it matches the data types!
    $stmt->bind_param("siiiiiiii", $sPlayerName, $iLevel, $iXP, $iCash, $iTimesPlayed, $iJumpUpgrade, $iTimeUpgrade, $ChangeColour, $MusicPlaying);
    //Execute statement
    $stmt->execute();
    echo "<p>Num rows added:$stmt->affected_rows";
}
else{
    //Prepare statement..? denotes to link to php variables later
    $query3 ="update tb_playerstats set level=?, xp=?, cash=?, timesplayed=?, jumpUpgrade=?, timeUpgrade=?, ChangeColour=?, CurrentlyPlaying=? where username=?";
    $stmt=$conn->prepare($query3);
    //s - string, i - integer...make sure it matches the data types!
    $stmt->bind_param("iiiiiiiis", $iLevel, $iXP, $iCash, $iTimesPlayed, $iJumpUpgrade, $iTimeUpgrade, $ChangeColour, $MusicPlaying, $sPlayerName);
    //Execute statement
    $stmt->execute();
}
$stmt->close();
$conn->close();
?>


