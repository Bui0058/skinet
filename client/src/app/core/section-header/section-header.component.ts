import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-section-header',
  templateUrl: './section-header.component.html',
  styleUrls: ['./section-header.component.scss']
})
export class SectionHeaderComponent implements OnInit {
  breadcrumb$ : Observable<any[]>; //tail wiht $ if an observalbe
  constructor(private bcService: BreadcrumbService) { }
  ngOnInit(): void {
    //we don't know about the destroy of this observeble 
    this. breadcrumb$ = this.bcService.breadcrumbs$;
  }
}
