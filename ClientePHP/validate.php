<?php
    if (isset($_POST["UserName"])) 
	{
		if (!empty($_POST["UserName"]))  
		{	
			if(preg_match("/\w{1,10}/", $_POST["UserName"]))  
			{	
				echo "OK";
			} 
			else
			{
				echo "* Não é um UserName correcto (a-zA-Z0-9)";
			}
		}
		else
		{
			echo "* Introduza o UserName";
		}
	}  

    if (isset($_POST["Pass"])) 
	{
		if (!empty($_POST["Pass"]))  
		{
			if (strlen($_POST["Pass"])<7)  
			{
				echo "* Tem de ter mais de 7 caracters";
			}		
			else
			{
				echo "OK";
			}		
		}
		else
		{
			echo "* Introduza a PassWord (>7 caracters)";
		}
	}  
	
    if (isset($_POST["PlayerID"])) 
	{
		if (!empty($_POST["PlayerID"]))  
		{
			if (ctype_digit($_POST["PlayerID"])) 
			{	
				echo "OK";
			} 
			else
			{
				echo "* Não é um Inteiro Positivo";
			}
		}
		else
		{
			echo "* Introduza a Pontuação (int positivo)";
		}
	
	}  
	
    if (isset($_POST["MapID"])) 
	{
		if (!empty($_POST["MapID"]))  
		{	
			if (ctype_digit($_POST["MapID"]))  
			{	
				echo "OK";
			} 
			else
			{
				echo "* Não é um Inteiro Positivo";
			}
		}
		else
		{
			echo "* Introduza a Pontuação (int positivo)";
		}
	} 
	
    if (isset($_POST["Score"])) 
	{
		if (!empty($_POST["Score"]))  
		{	
			if (ctype_digit($_POST["Score"]))  
			{	
				echo "OK";
			} 
			else
			{
				echo "* Não é um Inteiro Positivo";
			}
		}
		else
		{
			echo "* Introduza a Pontuação (int positivo)";
		}
	}  
	
    if (isset($_POST["Date"])) 
	{
		if (!empty($_POST["Date"]))  
		{	
			if(preg_match("/\d{4}-\d{2}-\d{2} \d{2}:\d{2}/", $_POST["Date"]))  
			{	
				echo "OK";
			} 
			else
			{
				echo "* Não é uma Data Correcta (yyyy-mm-dd hh:mm)";
			}
		}
		else
		{
			echo "* Introduza a Data (yyyy-mm-dd hh:mm)";
		}	
	}  

?>
