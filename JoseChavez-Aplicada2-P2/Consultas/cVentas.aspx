<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="cVentas.aspx.cs" Inherits="JoseChavez_Aplicada2_P2.Consultas.cVentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3>Consulta de Ventas</h3>
            </div>
            <div class="panel-body">

                <div class="form-horizontal col-md-12" role="form">
                    <div class="form-group">
                        <label class="col-md-2 col-sm-2 col-xs-12 control-label input-sm" for="DropDLFiltro">Filtrar por:</label>
                        <div class="col-md-3 col-md-2 col-xs-12">
                            <asp:DropDownList ID="DropDLFiltro" CssClass="form-control input-sm" readOnly="true" runat="server">
                                <asp:ListItem Value="V.VentaId">Id</asp:ListItem>
                                <asp:ListItem Value="V.Monto">Monto</asp:ListItem>
                                <asp:ListItem Value="VD.ArticuloId">ArticuloId</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-4 col-xs-12">
                            <asp:TextBox ID="FiltroTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>

                        </div>
                        <div class="col-md-4 col-sm-4 col-xs-8">
                            <asp:LinkButton CssClass="btn btn-info btn-lg btn-block" ID="ButtonBuscar" runat="server" Width="36px" OnClick="ButtonBuscar_Click"><span class="glyphicon glyphicon-search">Buscar</span> </asp:LinkButton>
                        </div>
                    </div>
                    <div class="form-group">

                        <div class="col-md-4 col-md-4 col-xs-12">
                            <label class="col-md-2 col-sm-4 col-xs-12 control-label input-sm" for="DesdeTextBox">Desde:</label>
                            <asp:TextBox ID="DesdeTextBox" CssClass="form-control input-sm" runat="server" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-md-4 col-xs-12">
                           <label class="col-md-2 col-sm-4 col-xs-12 control-label input-sm" for="HastaTextBox">Hasta:</label>
                            <asp:TextBox ID="HastaTextBox" CssClass="form-control input-sm" runat="server" TextMode="Date"></asp:TextBox>

                        </div>
                        <div class="col-md-4  col-md-4 col-xs-8">
                            <label for="ActivaCheckBox">Activar Filtro por Fecha</label>
                            <asp:CheckBox ID="ActivaCheckBox" runat="server" />
                        </div>
                    </div>


                </div>
                <asp:GridView CssClass=" table table-responsive table-bordered table-hover" ID="VentasGridView" runat="server">
                    <Columns>
                        <asp:HyperLinkField
                            DataNavigateUrlFields="ID"
                            DataNavigateUrlFormatString="/Registro/rVentas.aspx?ID={0}"
                            Text="Ver"
                            ControlStyle-CssClass="label label-success" />
                    </Columns>
                </asp:GridView>

            </div>

            <hr />
            <div class="panel-footer">
            </div>
        </div>
    </div>
</asp:Content>
