var ContactActualWIndow = new ContactActualWIndowFunc();
var refreshedSectionId;


function ContactActualWIndowFunc() 
{
	this.RenderWindow = function (sectionID, modalCaption, dialogFirst, dialogSecond, okButtonCaption, cancelButtonCaption) {
       // alert("XXX123");
		var dialogWindow = document.getElementById('dialogoverlay');
		if(dialogWindow)
		{
		ContactActualWIndow.OpenWindow();
		return;
		}
		
		var contactName = window.document.title;
		refreshedSectionId = sectionID;
		var winW;
        var winH;
		
		var myNav = navigator.userAgent.toLowerCase();
			
		if(myNav == undefined)
		{
		/*if not IE8*/
			winW = window.innerWidth;
        	winH = window.innerHeight;
		}
		else if(parseInt(myNav.split('msie')[1]) == 8 || parseInt(myNav.split('msie')[1]) == 7)
		{
			/* if IE8 */
			winW = document.body.clientWidth;
        	winH = document.body.clientHeight;
  		}
		else
		{
			winW = window.innerWidth;
        	winH = window.innerHeight;
		}
		
		
				
		
		var dialogoverlay = document.createElement("div");
		dialogoverlay.id = "dialogoverlay";
		var dialogbox = document.createElement("div");
		dialogbox.id = "dialogbox";
		
		/*header div*/
		var dialogboxhead = document.createElement("div");
		dialogboxhead.id = "dialogboxhead";
		
		/*body div*/
		var dialogboxbody = document.createElement("div");
		dialogboxbody.id = "dialogboxbody";
		
		/*footer div*/
		var dialogboxfoot = document.createElement("div");
		dialogboxfoot.id = "dialogboxfoot";
							
		dialogoverlay.appendChild(dialogbox);
		dialogbox.appendChild(dialogboxhead);
		dialogbox.appendChild(dialogboxbody);
		dialogbox.appendChild(dialogboxfoot);
		
        	dialogoverlay.style.display = "block";
        	dialogoverlay.style.height = winH + "px"	
        	dialogbox.style.left = (winW / 2) - (550 * .5) + "px";
        	dialogbox.style.top = "100px";
        	dialogbox.style.display = "block";
        	dialogbox.style.verticalAlign = "top";
	
		document.body.appendChild(dialogoverlay);
		
        document.getElementById('dialogboxhead').innerHTML = ''+ modalCaption + '';
		document.getElementById('dialogboxbody').innerHTML = '' +  dialogFirst + '</br>' + contactName + '</br>' + dialogSecond + '';	
        document.getElementById('dialogboxfoot').innerHTML =' <table width="100%"> <tr> <td colspan="2" align="center" width="50%">  <button style="background-color:buttonface; width:160px;" onclick="ContactActualWIndow.ok();">'+ okButtonCaption + '</button> </td> 	<td align="center" width="50%">  <button style="background-color:buttonface; width:160px;" onclick="ContactActualWIndow.cancel()">' + cancelButtonCaption + '</button></td> </tr> </table> ';

		};

		this.cancel = function() {
			document.getElementById('dialogbox').style.display = "none";
			document.getElementById('dialogoverlay').style.display = "none";
		};
		this.ok = function() {
			ViewModel.dispatchModelCommand('SaveActualContact');
			refreshSection(refreshedSectionId, false, true);
			ContactActualWIndow.cancel();
		}; 
		this.OpenWindow = function () {
			document.getElementById('dialogbox').style.display = "block";
			document.getElementById('dialogoverlay').style.display = "block";
		}
	};
;


        
 





     
     	
			
     




