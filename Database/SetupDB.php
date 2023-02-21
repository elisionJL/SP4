<?php //DeleteAllScores.php
//check if POST fields are received, else quit
// Connect database 
include("dbconninc.php");
// Prepare Statement...? denotes to link to php variables later
$query=["
CREATE TABLE tb_towerselect (
            username varchar(100) NOT NULL,
            Tower1 INT NULL,
            Tower2 INT NULL,
            Tower3 INT NULL,
            Tower4 INT NULL,
            Tower5 INT NULL
);
","
CREATE TABLE tb_playerstats (
            HostagesLeft INT NOT NULL,
            LvlCleared INT NOT NULL
);
"];
foreach($query as $a){

if(mysqli_query($conn,$a)){
    echo "Table created<br>";
}else{
    echo "Broken".mysqli_error($conn);
}
}

$conn->close(); // Close connection

?>
