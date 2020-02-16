import { Component } from '@angular/core';
import { DataService } from './northwind.service';
import { Observable } from 'rxjs';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { DataSourceRequestState, DataResult } from '@progress/kendo-data-query';

@Component({
   selector: 'fetchdata',
   template: `
   <kendo-grid
       [data]="products"
       [pageSize]="state.take"
       [skip]="state.skip"
       [sort]="state.sort"
       [sortable]="true"
       [pageable]="true"
       [scrollable]="'scrollable'"
       [groupable]="true"
       [group]="state.group"
       [filterable]="true"
       [filter]="state.filter"
       (dataStateChange)="dataStateChange($event)"
       [height]="370">

       <!-- columns declaration removed for brevity -->

   </kendo-grid>
   `
})
export class FetchDataComponent {
   public products: GridDataResult;
   public state: DataSourceRequestState = {
       skip: 0,
       take: 5
   };

   constructor(private dataService: DataService) {
       this.dataService.fetch(this.state).subscribe(r => this.products = r);
   }

   public dataStateChange(state: DataStateChangeEvent): void {
       this.state = state;
       this.dataService.fetch(state)
           .subscribe(r => this.products = r);
   }
}