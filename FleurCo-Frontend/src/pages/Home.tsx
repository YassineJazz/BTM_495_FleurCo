import Layout from "../components/Layout";

export const Home = () => {
    return (
        <Layout>
            <h1 class="font-bold text-3xl">Welcome to FleurCo</h1>
            <a href="/products" class="btn btn-primary">Go to Products</a>
        </Layout>
    );
};

export default Home;
