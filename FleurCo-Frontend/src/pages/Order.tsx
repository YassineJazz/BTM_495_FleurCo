import { useNavigate, useParams } from "@solidjs/router";
import Layout from "../components/Layout";
import { createResource, For } from "solid-js";
import { Order, OrderProduct, getOrder, getOrderProducts } from "../utils/api";
import { convertToEasternTime } from "../utils/date";

const OrderPage = () => {
    const params = useParams();
    const navigate = useNavigate();

    const [order] = createResource<Order>(() => getOrder(params.orderId));
    const [products] = createResource<OrderProduct[]>(() => getOrderProducts(params.orderId));

    const goBack = () => {
        navigate("/orders");
    }

    return (
        <Layout>
            <div class="flex w-full justify-between items-center gap-4">
                <div class="flex flex-col h-full w-full gap-4">
                    <div class="px-2 flex flex-col">
                        <h1 class="text-3xl font-bold"> FleurCo </h1>
                        <div class="flex flex-row justify-between items-center">
                            <h2 class="text-3xl font-semi-bold"> Product Line </h2>
                            <div class="flex flex-row justify-center items-center">
                                <svg xmlns="http://www.w3.org/2000/svg" width="36" height="36" viewBox="0 0 24 24"><path d="M19 7.001c0 3.865-3.134 7-7 7s-7-3.135-7-7c0-3.867 3.134-7.001 7-7.001s7 3.134 7 7.001zm-1.598 7.18c-1.506 1.137-3.374 1.82-5.402 1.82-2.03 0-3.899-.685-5.407-1.822-4.072 1.793-6.593 7.376-6.593 9.821h24c0-2.423-2.6-8.006-6.598-9.819z" /></svg>                            <div class="flex flex-col justify-start items-start">
                                    <label class="text-3x1 font-bold text-base"> Bryan Doe</label>
                                    <label class="text-3x1 font-semi-bold text-sm">Warehouse Manager</label>
                                </div>
                            </div>

                        </div>
                        <div class="fflex flex-col gap-4 items-start">
                            <button class="btn btn-sm btn-neutral" onClick={goBack}>Back</button>
                            <p class="text-2xl font-semibold">Order info: {order()?.orderId}</p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="flex flex-col gap w-full h-full">
                <div class="flex flex-col">
                    <label for="date" class="label"> Date: {order()?.orderDate ? convertToEasternTime(order()?.orderDate!) : ""}</label>
                </div>
                <div class="flex flex-col">
                    <label for="type" class="label"> Type: {order()?.orderType}</label>
                </div>
                <div class="flex flex-col">
                    <label for="date" class="label"> Status: {order()?.orderStatus}</label>
                </div>
                <div class="flex flex-col">
                    <label for="date" class="label"> Total {order()?.orderTotal}</label>
                </div>

                <h1 class="text-lg font-semibold"> Products </h1>

                <div class="overflow-x-auto p-2 h-full">
                    <table class="table">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Quantity</th>
                                <th>Price</th>
                                <th>Cost</th>
                                <th>Category</th>
                            </tr>
                        </thead>
                        <tbody>
                            <For each={products()}>
                                {(product) => (
                                    <tr
                                    >
                                        <td>{product.productName}</td>
                                        <td>{product.productQty}</td>
                                        <td>{product.productPrice}</td>
                                        <td>{product.productCost}</td>
                                        <td>{product.productCategory}</td>
                                    </tr>
                                )}
                            </For>
                        </tbody>
                    </table>
                </div>
            </div>
        </Layout>
    )
}
export default OrderPage;

