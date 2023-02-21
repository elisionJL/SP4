<?php //ReadScoreBoardJSON.php
// Connect database
include("dbconninc.php");
// Prepare Statement (SQL query)
$query="select username,score from tb_leaderboard";
$stmt=$conn->prepare($query);
// Execute Statement
$stmt->execute();
//Link results to variables
$stmt->bind_result($sPlayerName,$iScore);
$arr=Array(); //JSON use: create main array
$arr["scores"]=Array(); //JSON use: create associate array item
//Fetch results
while($stmt->fetch()){
//JSON use: create associative array for each record //4json
$oneScore=array("username"=>$sPlayerName,"score"=>$iScore);
//JSON use: add to main index array
array_push($arr["scores"],$oneScore); //corrected typo
}
$stmt->close(); // Close Statement
$conn->close(); // Close connection
http_response_code(200); //JSON use: tell the client everything ok //4json
echo json_encode($arr); //JSON use: encode the json format
?>