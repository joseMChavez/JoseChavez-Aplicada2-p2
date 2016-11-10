﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="rVentas.aspx.cs" Inherits="JoseChavez_Aplicada2_P2.rVentas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 176px;
        }
        .auto-style2 {
        width: 202px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12 col-lg-12 col-xs-12 col-sm-12">
                <div class=" panel panel-primary">
                    <div class="panel panel-heading">
                        <h4>Ventas Online</h4>
                    </div>
                    <div class ="panel panel-body">
                        <div class="form-horizontal">
                            <div class =" form-group">
                                <div class =" input-sm form-control col-xs-3 col-sm-3">
                                    <label for="IdTextBox">Id</label>
                                    <asp:TextBox ID="IdTextBox" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class =" form-group">
                                <div class =" input-sm form-control col-xs-3 col-sm-3">
                                    <label for="FechaTextBoxx">Fecha:</label>
                                    <asp:TextBox ID="FechaTextBox" runat="server" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>
                            <div class =" form-group">
                                <div class =" input-sm form-control col-xs-3 col-sm-3">
                                    <label for="MontoTextBox">Monto:</label>
                                    <asp:TextBox ID="MontoTextBox" runat="server"></asp:TextBox>
                                </div>
                            
                                
                            
                            </div>
                            <table style="width:100%;">
                                    <tr>
                                        <td class="auto-style1">Articulo</td>
                                        <td class="auto-style2">Cantidad</td>
                                        <td>Precio</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1">
                                            <asp:DropDownList ID="ArticuloDropDownList" runat="server" AutoPostBack="True" Height="16px" Width="238px">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="CantidadTextBox" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="PrecioDropDownList" runat="server" Height="16px" Width="238px">
                                            </asp:DropDownList>
                                            <asp:Button ID="AddButton" runat="server" CssClass=" btn btn-primary" Text="Add" OnClick="AddButton_Click" />
                                        </td>
                                    </tr>
                                    
                                </table>
                        </div>
                        <asp:GridView ID="VentaGridView" runat="server" Width="590px">
                        </asp:GridView>
                    </div>
                    <div class="panel panel-footer">
                        <div class=" container">
                            <asp:Button ID="NuevoButton" runat="server" Text="Nuevo" OnClick="NuevoButton_Click" />
                            <asp:Button ID="GuardarButton" runat="server" Text="Guardar" OnClick="GuardarButton_Click" />
                            <asp:Button ID="EliminarButton" runat="server" Text="Eliminar" OnClick="EliminarButton_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>