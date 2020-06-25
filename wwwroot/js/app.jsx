let chart=null;

class ChartWindow extends React.Component {
    constructor(props) {
        super(props);
    }

    render(){
        return (
            <div className="chart-container">
                <canvas id={this.props.chartId}></canvas>
                </div>
        );
    }
    componentDidMount() {
        let ctx = document.getElementById(this.props.chartId);
        chart = new Chart(ctx,
            {
                type: "line",
                data:
                {
                    labelsp: [],
                    datasets:
                        [{
                            label: 'f(x) = aX^2 + bX + c',
                            data: [],
                            borderColor: 'blue',
                            borderWidth: 2,
                            fill: false
                        }]
                },
                options:
                {
                    responsitive: true,
                    scales:
                    {
                        xAxes:
                            [{
                                display: true
                            }],
                        yAxes:
                            [{
                                display: true
                            }]
                    }
                }
            });
    }
}



class ChartInputForm extends React.Component {

    constructor(props){
        super(props);
        this.btnSubmitOnClick = this.btnSubmitOnClick.bind(this);
    }

    btnSubmitOnClick(e) {
        e.preventDefault();
        let data = $('#form').serialize();
        $.ajax({
            method: 'POST',
            data: data,
            url: '/Home/FunctionAjax/',
            success: function (points) {
                if (points !== null) {
                    chart.data.labels = [];
                    chart.data.datasets[0].data = [];
                    for (let i = 0; i < points.length; i++) {
                        chart.data.labels.push(points[i].x);
                        chart.data.datasets[0].data.push(points[i].y);
                    }
                    chart.update();
                }
            }
        });
    }

    render() {
        return (
            <form id="form">
                <label className="title">Function: y = ax^2 + bx + c</label>
                <div className="flex-row">
                    <div>
                        <label>a:</label> <input type="number" name="a" defaultValue="5" />
                    </div>
                    <div>
                        <label>b:</label><input type="number" name="b" defaultValue="5" />
                    </div>
                    <div>
                        <label>c:</label><input type="number" name="c" defaultValue="16" />
                    </div>
                    <div>
                        <label>Step:</label><input type="number" name="step" defaultValue="1" />
                    </div>
                    <div>
                        <label>From:</label> <input type="number" name="from" defaultValue="-10" />
                    </div>
                    <div>
                        <label>To:</label><input type="number" name="to" defaultValue="10" />
                    </div>
                </div>
                <button id="btn" type="submit" onClick={this.btnSubmitOnClick} >Plot</button>
            </form>
        );
    }
}

ReactDOM.render(
    <ChartWindow chartId="chart" />,
    document.getElementById("chart-box")
);
ReactDOM.render(
    <ChartInputForm />,
    document.getElementById("input-box")
);