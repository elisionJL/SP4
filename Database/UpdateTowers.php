<?php //ReadScoreboard.php
// Connect database
include("dbconninc.php");
// Prepare Statement (SQL query)
if(!isset($_POST["username"]) || !isset($_POST["Tower1"]) || !isset($_POST["Tower2"]) || !isset($_POST["Tower3"])
|| !isset($_POST["Tower4"]) || !isset($_POST["Tower5"]))die("not posted!");

$sPlayerName=$_POST["username"];
$Tower1Get=$_POST["Tower1"];
$Tower2Get=$_POST["Tower2"];
$Tower3Get=$_POST["Tower3"];
$Tower4Get=$_POST["Tower4"];
$Tower5Get=$_POST["Tower5"];

$query="select Username from tb_towerselect where Username=?";
$stmt=$conn->prepare($query);
$stmt->bind_param("s", $sPlayerName);
$stmt->execute();
$stmt->store_result();
$row=$stmt->num_rows();
$stmt->bind_result($sPlayerName);
$stmt->fetch();

if($row==0){
    //Prepare statement..? denotes to link to php variables later
    $query2="insert into tb_towerselect (Username, Tower1, Tower2, Tower3, Tower4, Tower5) values (?,?,?,?,?,?)";
    $stmt=$conn->prepare($query2);
    //s - string, i - integer...make sure it matches the data types!
    $stmt->bind_param("siiiii", $sPlayerName, $Tower1Get, $Tower2Get, $Tower3Get, $Tower4Get, $Tower5Get);
    //Execute statement
    $stmt->execute();
    echo "<p>Num rows added:$stmt->affected_rows";
}
else{
    //Prepare statement..? denotes to link to php variables later
    $query3 ="update tb_towerselect set Tower1=?, Tower2=?, Tower3=?, Tower4=?, Tower5=? where username=?";
    $stmt=$conn->prepare($query3);
    //s - string, i - integer...make sure it matches the data types!
    $stmt->bind_param("iiiiis", $Tower1Get, $Tower2Get, $Tower3Get, $Tower4Get, $Tower5Get, $sPlayerName);
    //Execute statement
    $stmt->execute();
}
$stmt->close();
$conn->close();
?>


