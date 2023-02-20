<?php //ReadScoreboard.php
// Connect database
include("dbconninc.php");
// Prepare Statement (SQL query)
if(!isset($_POST["username"]))die("not posted!");
$sPlayerName=$_POST["username"];
$iLevel=1;
$iXP=0;
$iCash=0;
$iTimesPlayed=0;
$iJumpUpgrade=0;
$iTimeUpgrade=0;
$ColourChange=0;
$MusicPlaying=0;

$query="select Username from tb_playerstats where Username=?";
$stmt=$conn->prepare($query);
$stmt->bind_param("s", $sPlayerName);
$stmt->execute();
$stmt->store_result();
$row=$stmt->num_rows();
$stmt->bind_result($sPlayerName);
$stmt->fetch();
if($row==0){

}
else{
    $query2="select Username,Level,XP,cash,timesplayed,jumpUpgrade,timeUpgrade,ChangeColour,CurrentlyPlaying from tb_playerstats where Username=?";
    $stmt=$conn->prepare($query2);
    //s - string, i - integer...make sure it matches the data types!
    $stmt->bind_param("s", $sPlayerName);
    // Execute Statement
    $stmt->execute();
    //Link results to variables
    $stmt->bind_result($sPlayerName, $iLevel, $iXP, $iCash, $iTimesPlayed, $iJumpUpgrade, $iTimeUpgrade, $ChangeColour, $MusicPlaying);   
    $arr=Array(); //JSON use: create main array
    //Fetch results
    while($stmt->fetch()){
    //JSON use: create associative array for each record //4json
    $oneScore=array("username"=>$sPlayerName,"level"=>$iLevel,"xp"=>$iXP,"cash"=>$iCash,"TotalTimesPlayed"=>$iTimesPlayed,"JumpUP"=>$iJumpUpgrade,"TimeUP"=>$iTimeUpgrade,"CanChangeColour"=>$ChangeColour,"MusicCurrentlyPlaying"=>$MusicPlaying);
    //JSON use: add to main index array
    array_push($arr,$oneScore); //corrected typo
    }
    $stmt->close(); // Close Statement
    $conn->close(); // Close connection
    http_response_code(200); //JSON use: tell the client everything ok //4json
    echo json_encode($arr[0]); //JSON use: encode the json format
}
?>