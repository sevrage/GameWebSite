<?php
/*
echo $_POST["UserName"];
echo $_POST["Pass"];
echo $_POST["PlayerID"];
echo $_POST["MapID"];
echo $_POST["Score"];
echo $_POST["Date"];
*/

$client = new SoapClient("http://localhost:1198/GWebService.asmx?WSDL");
#var_dump($client->__getFunctions());
 
 $params= array('UserName' => $_POST["UserName"],
				'Pass' => $_POST["Pass"],
				'PlayerID' => $_POST["PlayerID"],
				'MapID' => $_POST["MapID"],
				'Score' => $_POST["Score"],
				'Date' => $_POST["Date"],				
				);
								
	try {
		$result = $client->SetScore($params);
		echo $result->SetScoreResult;	
	}
		catch (SoapFault $fault)
	{
		trigger_error("SOAP Fault: (faultcode: {$fault->faultcode}\n" .
		"faultstring: {$fault->faultstring})", E_USER_ERROR);
	}

?>