import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

import { GridModule } from '@progress/kendo-angular-grid';

import { FetchDataComponent } from './app.component';
import { DataService } from './northwind.service';

@NgModule({
  imports: [ HttpClientModule, FormsModule, BrowserModule, BrowserAnimationsModule, GridModule ],
  declarations: [ FetchDataComponent ],
  providers: [ DataService ],
  bootstrap: [ FetchDataComponent ]
})

export class AppModule { }
