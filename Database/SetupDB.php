<?php //SetupDB.php
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
    username varchar(100) NOT NULL,
    HostagesLeft INT Default 100,
    LvlCleared TinyINT Default 0,
    TimePlayed INT Default 0
);
","
CREATE TABLE tb_OwnedSkins (
            username varchar(100) NOT NULL,
            PlayerSkins varchar(10) DEFAULT 'B'
);
","
CREATE TABLE tb_Settings (
    username varchar(100) NOT NULL,
    MasterVolume TINYINT DEFAULT 100,
    SFXVolume TINYINT DEFAULT 100,
    BGMVolume TINYINT DEFAULT 100
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