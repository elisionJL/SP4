<?php //DeleteAllScores.php
//check if POST fields are received, else quit

// Connect database 
include("dbconninc.php");
$query="delete from tb_leaderboard";
$stmt=$conn->prepare($query);
// Execute Statement
$stmt->execute();
$stmt->close(); // Close Statement
$conn->close(); // Close connection
http_response_code(200); //JSON use: tell the client everything ok //4json
echo json_encode($arr); //JSON use: encode the json format
?>