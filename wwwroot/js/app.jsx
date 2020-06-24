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
                            label: 'f(x)',
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
                <label htmlFor="function">Function:</label>
                <div id="function">
                    <span>y=</span>
                    <input type="number" name="a" defaultValue="5" />
                    <span>x^2+</span>
                    <input type="number" name="b" defaultValue="5" />
                    <span>x+</span>
                    <input type="number" name="c" defaultValue="16" />
                </div>
                <label htmlFor="step">Step:</label>
                <div id="step">
                    <input type="number" name="step" defaultValue="1" />
                </div>
                <label htmlFor="from">From:</label>
                <div id="from">
                    <input type="number" name="from" defaultValue="-10" />
                    <span>to</span>
                    <input type="number" name="to" defaultValue="10" />
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