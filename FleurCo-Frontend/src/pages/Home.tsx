import Layout from "../components/Layout";

export const Home = () => {
    return (
        <Layout>
            <div class="flex flex-col w-full gap-8 p-4 h-full">
                <h1 class="font-bold text-3xl text-left">Welcome to FleurCo Inventory System</h1>
                <div class="flex flex-wrap gap-8 pt-56 justify-center items-center">
                    <a href="/inventory" class="btn btn-lg btn-primary w-48 h-20 text-lg">Inventory</a>
                    <a href="/products" class="btn btn-lg btn-primary w-48 h-20 text-lg">Product Line</a>
                    <a href="/orders" class="btn btn-lg btn-primary w-48 h-20 text-lg">Orders</a>
                </div>
            </div>
        </Layout>
    );
};

export default Home;
