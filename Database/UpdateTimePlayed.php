<?php //ReadScoreboard.php
// Connect database
include("dbconninc.php");
// Prepare Statement (SQL query)
if(!isset($_POST["username"]) || !isset($_POST["TimePlayed"]))die("not posted!");

$sPlayerName=$_POST["username"];
$sTimePlayed=$_POST["TimePlayed"];

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
    $query2="insert into tb_playerstats (Username, TimePlayed) values (?,?)";
    $stmt=$conn->prepare($query2);
    //s - string, i - integer...make sure it matches the data types!
    $stmt->bind_param("ss", $sPlayerName, $sTimePlayed);
    //Execute statement
    $stmt->execute();
    echo "<p>Num rows added:$stmt->affected_rows";
}
else{
    //Prepare statement..? denotes to link to php variables later
    $query3 ="update tb_Playerstats set TimePlayed = ? where username=?";
    $stmt=$conn->prepare($query3);
    //s - string, i - integer...make sure it matches the data types!
    $stmt->bind_param("ss", $sTimePlayed, $sPlayerName);
    //Execute statement
    $stmt->execute();
    echo $sTimePlayed;
}
$stmt->close();
$conn->close();
?>


