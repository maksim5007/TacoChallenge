import {
  Component,
  OnInit,
  ViewEncapsulation,
  Injector,
  Renderer2
} from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { SearchServiceProxy, SearchResultDto, MenuItemDto } from '@shared/service-proxies/service-proxies';
import * as _ from 'lodash';

@Component({
  templateUrl: './search.component.html',
  encapsulation: ViewEncapsulation.None,
  styleUrls: ['./search.component.less']
})

export class SearchComponent extends AppComponentBase implements OnInit {

  constructor(
    injector: Injector,
    private _searchService: SearchServiceProxy,
    private renderer: Renderer2
  ) {
    super(injector);
  }

  searchText = '';
  searchResults: SearchResultDto[] = [];
  availableMenuItems: MenuItemDto[] = [];
  selectedMenuItems: MenuItemDto[] = [];
  totlalPrice = 0.0;

  showTenantChange(): boolean {
    return abp.multiTenancy.isEnabled;
  }

  ngOnInit(): void {
    this.renderer.addClass(document.body, 'search-page');
  }

  search(): void {    
    this.clearSearchResults();
    this._searchService.search(this.searchText).subscribe(result => {
      this.searchResults = result;
      let menuItems = _.flatten(_.map(this.searchResults, 'menuItems'));
      let categorizedMenuItems = _.flattenDeep(_.map(this.searchResults, n => {
        return _.map(n.categoryGroups, 'menuItems');
      }));

      this.availableMenuItems = _.uniqBy(_.concat(menuItems, categorizedMenuItems), 'id');
    });
  }

  selectionChanged(id: number, event: any) {
    if (event.srcElement.checked) {
      let item = _.find(this.availableMenuItems, n => { return n.id == id; });

      if (item && _.every(this.selectedMenuItems, n => { return n.id != id; })) {
        this.selectedMenuItems.push(item);
      }
    } else {
      var indexToRemove = _.findIndex(this.selectedMenuItems, n => { return n.id == id; })
      if (indexToRemove >= 0) {
        this.selectedMenuItems.splice(indexToRemove, 1);
      }
    }

    this.totlalPrice = _.sumBy(this.selectedMenuItems, n => { return n.price; });
  }

  makeOrder() {
    this._searchService.createOrder(this.selectedMenuItems).subscribe(() => {
      this.message.success(this.l("OrderSuccessfulMessage"), this.l("Success"))
      this.clearSearchResults();
    });
  }

  clearSearchResults() {
    this.searchResults = [];
    this.availableMenuItems = [];
    this.selectedMenuItems = [];
    this.totlalPrice = 0 ;
  }

}
