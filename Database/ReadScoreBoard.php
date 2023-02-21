<?php //ReadScoreboard.php
// Connect database
include("dbconninc.php");
$query="select sPlayerName,iScore from tb_scoreboard order by iScore Desc";
$stmt=$conn->prepare($query);
// Execute Statement
$stmt->execute();
//Bind results to variables
$stmt->bind_result($sPlayerName,$iScore);
// Fetch Results (select)
while($stmt->fetch()){
    echo "$sPlayerName:$iScore<br>";
}
// Close Statement
$stmt->close();
// Close connection
$conn->close();
?>