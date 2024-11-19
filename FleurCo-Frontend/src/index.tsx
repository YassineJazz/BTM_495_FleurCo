/* @refresh reload */
import { render } from 'solid-js/web';
import { Route, Router } from "@solidjs/router";
import './index.css';
import NotFound from './pages/Notfound';
import Products from './pages/Products';
import Product from './pages/Product';
import Home from './pages/Home';
import NewProductPage from './pages/NewProduct';
import Orders from './pages/Orders';
import Inventory from './pages/Inventory';
import Item from './pages/item';

const root = document.getElementById('root');

if (import.meta.env.DEV && !(root instanceof HTMLElement)) {
    throw new Error(
        'Root element not found. Did you forget to add it to your index.html? Or maybe the id attribute got misspelled?',
    );
}

render(() => (
    <Router>
        <Route path="/" component={Home} />
        <Route path="/inventory" component={Inventory} />
        <Route path="/inventory/:inventoryId" component={Item} />
        <Route path="/products" component={Products} />
        <Route path="/products/:productId" component={Product} />
        <Route path="/products/new" component={NewProductPage} />
        <Route path="/orders" component={Orders} />
        <Route path="*" component={NotFound} />
    </Router>
), root!);
