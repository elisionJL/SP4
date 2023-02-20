<?php //ReadScoreboard.php
// Connect database
include("dbconninc.php");
// Prepare Statement (SQL query)
if(!isset($_POST["username"]) || !isset($_POST["Music1"]) || !isset($_POST["Music2"]) || !isset($_POST["Music3"])
|| !isset($_POST["Music4"])|| !isset($_POST["Music5"]))die("not posted!");
$sPlayerName=$_POST["username"];
$Music_1=$_POST["Music1"];
$Music_2=$_POST["Music2"];
$Music_3=$_POST["Music3"];
$Music_4=$_POST["Music4"];
$Music_5=$_POST["Music5"];

$query="select Username from tb_music where Username=?";
$stmt=$conn->prepare($query);
$stmt->bind_param("s", $sPlayerName);
$stmt->execute();
$stmt->store_result();
$row=$stmt->num_rows();
$stmt->bind_result($sPlayerName);
$stmt->fetch();

if($row==0){
    //Prepare statement..? denotes to link to php variables later
    $query2="insert into tb_music (Username, Music_1, Music_2, Music_3, Music_4, Music_5) values (?,?,?,?,?,?)";
    $stmt=$conn->prepare($query2);
    //s - string, i - integer...make sure it matches the data types!
    $stmt->bind_param("siiiii", $sPlayerName, $Music_1, $Music_2, $Music_3, $Music_4, $Music_5);
    //Execute statement
    $stmt->execute();
    echo "<p>Num rows added:$stmt->affected_rows";
}
else{
    //Prepare statement..? denotes to link to php variables later
    $query3 ="update tb_music set Music_1=?, Music_2=?, Music_3=?, Music_4=?, Music_5=? where username=?";
    $stmt=$conn->prepare($query3);
    //s - string, i - integer...make sure it matches the data types!
    $stmt->bind_param("iiiiis", $Music_1, $Music_2, $Music_3, $Music_4, $Music_5, $sPlayerName);
    //Execute statement
    $stmt->execute();
}
$stmt->close();
$conn->close();
?>


