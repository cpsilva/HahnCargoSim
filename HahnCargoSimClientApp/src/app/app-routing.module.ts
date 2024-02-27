import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from '@modules/main/main.component';
import { SimulationComponent } from '@pages/simulation/simulation.component';
import { LoginComponent } from '@modules/login/login.component';
import { ProfileComponent } from '@pages/profile/profile.component';
import { HomeComponent } from '@pages/home/home.component';
import { AuthGuard } from '@guards/auth.guard';
import { NonAuthGuard } from '@guards/non-auth.guard';
import { LogComponent } from '@pages/simulation-logs/log.component';

const routes: Routes = [
    {
        path: '',
        component: MainComponent,
        canActivate: [AuthGuard],
        canActivateChild: [AuthGuard],
        children: [
            {
                path: 'profile',
                component: ProfileComponent
            },
            {
                path: 'simulation',
                component: SimulationComponent
            },
            {
                path: 'logs',
                component: LogComponent
            },
            {
                path: '',
                component: HomeComponent
            }
        ]
    },
    {
        path: 'login',
        component: LoginComponent,
        canActivate: [NonAuthGuard]
    },
    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, {})],
    exports: [RouterModule]
})
export class AppRoutingModule { }
