<?php //ReadSettings
// Connect database
include("dbconninc.php");
if (!isset($_POST["username"]))die("not posted!");
$sUsername = $_POST["username"];
// Prepare Statement (SQL query)
$query="select Username,MasterVolume, SFXVolume,BGMVolume from tb_settings where Username =?;";
$stmt=$conn->prepare($query);
$stmt->bind_param("s", $sUsername);
// Execute Statement
$stmt->execute();
// Bind results(Select)/parameters (insert/delete/update)
$stmt->bind_result($sUsername, $sNewMaster,$sNewSFX,$sNewSFX);
$stmt->store_result();
$rows = $stmt->num_rows();
if ($rows != 0) {
  $arr = array(); //JSON use: create main array

  // Fetch Results (select) / Get Rows affected (ins/del/upd)
  while ($stmt->fetch()) {
    //JSON use: create associative array for each record //4json
    $playerStats = array("username" => $sUsername, "masterVolume" => $sNewMaster, "sfxVolume" => $sNewSFX, "bgmVolume" => $sNewSFX);
    array_push($arr, $playerStats);
  }
}
// Close Statement
$stmt->close();
// Close connection
$conn->close();     
if ($rows != 0){
http_response_code(200);//JSON use: tell the client everything ok // 4json
echo json_encode($arr[0]);//JSON use: encode the json format
}
?>