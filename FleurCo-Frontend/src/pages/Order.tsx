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
            <div class="flex w-full justify-between items-center">
                <div class="flex gap-4">
                    <button class="btn btn-sm btn-neutral" onClick={goBack}>Back</button>
                    <p class="text-2xl font-semibold">Order info: {order()?.orderId}</p>
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

