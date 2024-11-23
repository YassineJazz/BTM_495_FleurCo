import Layout from "../components/Layout";

export const Home = () => {
    return (
        <Layout>
            <h1 class="font-bold text-3xl">Welcome to FleurCo Inventory System</h1>
            <div class="flex flex-col gap-8 w-full h-full justify-center items-center">
                <div class="flex gap-8 justify-center items-center">
                    <a href="/inventory" class="btn btn-lg btn-primary w-48 h-20 text-lg">Inventory</a>
                    <a href="/products" class="btn btn-lg btn-primary w-48 h-20 text-lg">Product Line</a>
                    <a href="/orders" class="btn btn-lg btn-primary w-48 h-20 text-lg">Orders</a>
                </div>
            </div>
        </Layout >
    );
};

export default Home;
