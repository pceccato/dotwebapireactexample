class WeatherPanel extends React.Component {

    render() {

        if(this.props.weather) {
            return (
                    <div className="panel panel-default">
                        <div className="panel-heading">Weather conditions for {this.props.weather.location} ({this.props.city})</div>
                        <div className="panel-body">
                            <dl className='dl-horizontal'>
                                <dt>Time</dt>
                                <dd>{this.props.weather.time}</dd>
                                <dt>Wind</dt>
                                <dd>{this.props.weather.wind}</dd>
                                <dt>Visibility</dt>
                                <dd>{this.props.weather.visibility}</dd>
                                <dt>Sky Conditions</dt>
                                <dd>{this.props.weather.skyConditions}</dd>
                                <dt>Temp</dt>
                                <dd>{this.props.weather.temperature}</dd>
                                <dt>Dew point</dt>
                                <dd>{this.props.weather.dewPoint}</dd>
                                <dt>Relative humidity</dt>
                                <dd>{this.props.weather.humidity}</dd>
                                <dt>Atmospheric Pressure</dt>
                                <dd>{this.props.weather.pressure}</dd>
                            </dl>
                        </div>
                </div>
                )

        }
        return null
    }

}


class WeatherAPIClient extends React.Component {

    constructor(props) {
            super(props);
            this.state = { country: ''}
    }

    callGetCities = () => {
        let endpoint = '/api/cities?country=' + encodeURIComponent(this.state.country)
        let that = this
        $.get(endpoint)
        .done(function(data) {
            that.setState({cities: data })
        })
        .fail(function(jqXHR) {
            if(jqXHR.status == 404) {
                alert("country not found!")
            }
            else {
                alert( "error calling cities API" );
            }
        })
    }

        callGetWeatherForCity = (city) => {

        const endpoint = '/api/weather/?country=' + encodeURIComponent(this.state.country) + '&city=' + encodeURIComponent(city)
        const that = this
        $.get(endpoint)
        .done(function(data) {
            that.setState({weather: data })
        })
        .fail(function(jqXHR) {
            if(jqXHR.status == 404) {
                alert("city not found!")
            }
            else {
                alert( "error calling weather for city API" );
            }
        })
    }

	handleKeyPress = (e) => {
		if (e.key === 'Enter') {
		    this.setState({cities: null, weather: null})
            this.callGetCities()
		}
	}

	handleChange = (e) => {
	    this.setState({country: e.target.value});
	}

	handleCityChange = (e) => {
        const city = e.target.value
	    this.setState({city: city})
        this.callGetWeatherForCity(city)
	}

	render() {
        let citiesSelect = null
	    if(this.state.cities) {
	        const options =  this.state.cities.map((city) => <option value={city}>{city}</option> )
	        citiesSelect = <select className="form-control" onChange={this.handleCityChange}><option selected disabled>Select a city..</option>{options}</select>
	    }

    return (    
        <div>
            <div className='row'>
                <input className="form-control" type="text" onKeyPress={this.handleKeyPress} onChange={this.handleChange} placeholder='Enter Country and hit return' />
            </div>
            <br/>
            <div className='row'>
                {citiesSelect}
            </div>
		    <br/>
            <div className='row'>
                <WeatherPanel weather={this.state.weather} city={this.state.city} />
            </div>
        </div>       
            )
	}
}


ReactDOM.render( <WeatherAPIClient />, document.getElementById('root'));


