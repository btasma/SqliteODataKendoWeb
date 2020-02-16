import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
    toDataSourceRequestString,
    translateDataSourceResultGroups,
    translateAggregateResults,
    DataResult,
    DataSourceRequestState
} from '@progress/kendo-data-query';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

@Injectable()
export class DataService {
    private BASE_URL: string = 'https://localhost:44393/Blogs/DSR';

    constructor(private http: HttpClient) { }

    public fetch(state: DataSourceRequestState): Observable<DataResult> {
        const queryStr = `${toDataSourceRequestString(state)}`; // Serialize the state
        const hasGroups = state.group && state.group.length;

        return this.http
            .get(`${this.BASE_URL}?${queryStr}`) // Send the state to the server
            .map(({data, total/*, aggregateResults*/} : GridDataResult) => // Process the response
                (<GridDataResult>{
                    // If there are groups, convert them to a compatible format
                    data: hasGroups ? translateDataSourceResultGroups(data) : data,
                    total: total,
                    // Convert the aggregates if such exist
                    //aggregateResult: translateAggregateResults(aggregateResults)
                })
            )
    }
}