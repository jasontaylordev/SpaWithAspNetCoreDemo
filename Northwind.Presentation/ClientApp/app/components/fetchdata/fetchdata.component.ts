import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { SampleDataService, WeatherForecast } from '../../services/services.generated';

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html',
    providers: [ SampleDataService ]
})
export class FetchDataComponent {
    public forecasts: WeatherForecast[];

    constructor(sampleDataService: SampleDataService) {
        sampleDataService.weatherForecasts().subscribe(
            result => this.forecasts = result
        );
    }
}
