import Layout from "../components/Layout";

const NotFound = () => {
    return (
        <Layout>
            <div class="flex flex-col items-center justify-start pt-20 h-screen container w-full mx-auto gap-4">
                <h1 class="font-bold text-3xl">404 - Page Not Found</h1>
                <a href="/" class="btn btn-primary">Go back home</a>
            </div>
        </Layout>
    );
};

export default NotFound;
