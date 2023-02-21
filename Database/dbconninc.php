<?php //dbconninc.php
// database settings
$hostaddress="localhost";
$dbuser="root";
$password="";
$dbname="OurGame"; //change to the dbname based on your naming convention
// Connect database
$conn=new mysqli($hostaddress,$dbuser,$password,$dbname);
?>
