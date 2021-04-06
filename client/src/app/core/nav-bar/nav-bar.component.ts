import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  basket$ : Observable<IBasket>;
  constructor(private basketService : BasketService) { }

  ngOnInit(): void {
    //then we use async to subcribe to this observable
    //in the template to get the basket value back
    this.basket$ = this.basketService.basket$;
  }

}
