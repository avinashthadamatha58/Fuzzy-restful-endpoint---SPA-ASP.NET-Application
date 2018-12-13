<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DragonFly Athletics</title>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
  <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/sunny/jquery-ui.css" />
        <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.js"></script>
        <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>
  <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css"
    rel="stylesheet" type="text/css" />
          
        <script src="scripts/JavaScript.js"></script>
  <link href="css/StyleSheet1.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
            <div>
                 
            <asp:Repeater ID="rptCustomers" runat="server">
    <HeaderTemplate>
        <table id="tblCustomers" class="footable" border="0">
            <thead>
                <tr>
                    <th data-class="expand">
                        Check Box
                    </th>
                    <th scope="col">
                       View Details from API
                    </th>
                    <th style="display: table-cell;" data-hide="phone">
                        Event Name
                    </th>
                    <th style="display: table-cell;" data-hide="phone,tablet">
                        Description
                    </th>
                    <th style="display: table-cell;" data-hide="phone">
                        Location
                    </th> 
                    <th style="display: table-cell;" data-hide="phone,tablet">
                        Thumbnail
                    </th> 
                </tr>
            </thead>
    </HeaderTemplate>
    <ItemTemplate>
                   <script type="text/javascript">
                       var eID = '<%#Eval("EventID")%>';
                       var iID = '<%#Eval("ImageID")%>';
                       executeAjax(eID, iID);
                    </script>
        <tbody>
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox1"  onchange='<%#string.Format("javascript:return saveData(\"{0}\")", Eval("EventID").ToString()) %>' Enabled="false" ToolTip="Once clicked, checkbox will be disabled, request will be queued and executed after getting all saved checkbox responses (only first time), so please wait until checkbox is enabled" runat="server" ></asp:CheckBox>
                        </td>
                <td>
                    <asp:HyperLink ID="btnEdit" Text="View sub details" onclick='<%#string.Format("javascript:return openModal(\"{0}\",\"{1}\")", Eval("EventID").ToString(), Eval("ImageID").ToString())%>'
                                CssClass="btn btn-info" runat="server" />
                <asp:HiddenField ID="hfCustomerId" runat="server" Value='<%#Eval("EventID")%>' />
            </td>
               <div hidden="hidden">
                     
                                    <span id="lblOrderId">'<%# Eval("EventID") %>'  </span>
                               
                                    <span id="EName_<%#Eval("EventID")%>"><%# Eval("EName") %>> </span>
                                
                                    <span id="EComments_<%#Eval("EventID")%>"><%# Eval("Comments") %> </span>
                               
                                    <span id="EDate_<%#Eval("EventID")%>"><%# Eval("Date") %> </span>
                               
                                   <img id="id1_<%#Eval("EventID")%>_<%#Eval("ImageID")%>" class="img" alt = "caption" src="images/load.gif" />

               </div>
                <td>
                    <%#Eval("EName")%>
                </td>
                <td>
                    <%#Eval("Description")%>
                </td>
                <td>
                    <%#Eval("Location")%>
                </td>
           <td>
                <img id="id_<%#Eval("EventID")%>_<%#Eval("ImageID")%>" onclick='<%#string.Format("javascript:return openModal(\"{0}\",\"{1}\")", Eval("EventID").ToString(), Eval("ImageID").ToString())%>' alt = "image not found" class="img-thumbnail" src="images/load.gif" />
           </td>
            </tr>
        </tbody>
    </ItemTemplate>
            
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
                 
                <div class="modal fade" id="addPage" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="text-align: center;" id="myModalLabel">
                        Event Details</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label>
                            Event ID : </label>
                        <span id="eventid" runat="server" class="form-eventid"> 
                             </span>
                    </div>
                    <div class="form-group">
                        <label>
                            Large Image : </label>
                        <img id="img1" src="images/load.gif" class="image" runat="server"/>
                        
                    </div>
                    <div class="form-group">
                        <label>
                            Comments : </label>
                        <span  id="eventComments" class="form-eventComments">
                             </span>
                    </div>
                    <div class="form-group">
                        <label>
                            Date : </label>
                        <span id="eventdate" class="form-eventDate">
                            </span>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
</div>                    
    </form>
</body>
</html>
