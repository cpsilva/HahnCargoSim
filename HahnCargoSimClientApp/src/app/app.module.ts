import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CommonModule, registerLocaleData } from '@angular/common';
import localeEn from '@angular/common/locales/en';
import { MainComponent } from './modules/main/main.component';
import { HeaderComponent } from './modules/main/header/header.component';
import { FooterComponent } from './modules/main/footer/footer.component';
import { SimulationComponent } from './pages/simulation/simulation.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { HomeComponent } from './pages/home/home.component';
import { NotificationsComponent } from './modules/main/header/notifications/notifications.component';
import { UserComponent } from './modules/main/header/user/user.component';
import { MenuItemComponent } from './components/menu-item/menu-item.component';
import { ProfabricComponentsModule } from '@profabric/angular-components';
import { StoreModule } from '@ngrx/store';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { authReducer } from './store/auth/reducer';
import { uiReducer } from './store/ui/reducer';
import { LoginComponent } from '@modules/login/login.component';
import { MenuSidebarComponent } from '@modules/main/menu-sidebar/menu-sidebar.component';
import { LogComponent } from '@pages/simulation-logs/log.component';

registerLocaleData(localeEn, 'en-EN');

@NgModule({
    declarations: [
        AppComponent,
        MainComponent,
        LoginComponent,
        HeaderComponent,
        FooterComponent,
        MenuSidebarComponent,
        SimulationComponent,
        ProfileComponent,
        HomeComponent,
        NotificationsComponent,
        UserComponent,
        MenuItemComponent,
        LogComponent
    ],
    imports: [
        ProfabricComponentsModule,
        CommonModule,
        BrowserModule,
        StoreModule.forRoot({ auth: authReducer, ui: uiReducer }),
        HttpClientModule,
        AppRoutingModule,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot({
            timeOut: 3000,
            positionClass: 'toast-top-right',
            preventDuplicates: true
        }),
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
