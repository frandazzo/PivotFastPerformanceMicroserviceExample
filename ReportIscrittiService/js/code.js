$(function () {

    var settori = ["","Edile","Impianti Fissi", "Inps"];
   
   

    var pivotGridChart = $("#pivotgrid-chart").dxChart({
        commonSeriesSettings: {
            type: "bar"
        },
        tooltip: {
            enabled: true,
            customizeTooltip: function (args) {
                var valueText = (args.seriesName.indexOf("Total") != -1) ?
                        Globalize.formatCurrency(args.originalValue,
                            "USD", { maximumFractionDigits: 0 }) :
                        args.originalValue;

                return {
                    html: args.seriesName + "<div class='currency'>"
                        + valueText + "</div>"
                };
            }
        },
        size: {
            height: 200
        },
        adaptiveLayout: {
            width: 450
        }
    }).dxChart("instance");





    var pivotGrid = $("#grid").dxPivotGrid({
        // height: 800,
        fieldChooser: {
            allowSearch: true
        },
        scrolling: { mode: "virtual" },
        fieldPanel: { visible: true },
        allowSorting: true,
        allowSortingBySummary: true,
        allowFiltering: true,
        showBorders: true,
        showColumnGrandTotals: false,
        showRowGrandTotals: true,
        showRowTotals: true,
        showColumnTotals: true,
        "export": {
            enabled: true,
            fileName: "ReportIscritti"
        },
        onCellClick: function(e) {
          
        },
        dataSource: {
            fields: [
                { dataField: "Anno", area: "column", sortByPath: [] },
                { dataField: "NomeRegione", area: "row",  sortOrder: "desc" },
                { dataField: "NomeProvincia", area: "row",  sortOrder: "desc" },
                { dataField: "Id_Lavoratore", caption: "Num. Lavoratori", summaryType: "count", area: "data" }
            ],
            remoteOperations: true,
            store: DevExpress.data.AspNet.createStore({
                key: "ID",
                loadUrl: "http://localhost:8080/api/devexpress"
            })
        }
    }).dxPivotGrid("instance");


    pivotGrid.bindChart(pivotGridChart, {
        dataFieldsDisplayMode: "splitPanes",
        alternateDataFields: false
    });

    $("#dropDownBox").dxDropDownBox({
        value: settori[0],
        dataSource: settori,
        contentTemplate: function (e) {
            var $list = $("<div>").dxList({
                dataSource: e.component.option("dataSource"),
                selectionMode: "single",
                onSelectionChanged: function (arg) {
                    var value = arg.addedItems[0];
                    e.component.option("value", value);
                    e.component.close();


                    console.log(value);


                    var fields = pivotGrid.getDataSource().fields();
                    pivotGrid.option("dataSource", {
                        fields: fields,
                         store: DevExpress.data.AspNet.createStore({
                            key: "ID",
                            loadUrl: "http://localhost:8080/api/devexpress?sector=" + encodeURIComponent(value)
                        })
                    })



                }
            });
            return $list;
        }
    });

});