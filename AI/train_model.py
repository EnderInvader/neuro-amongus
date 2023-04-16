import os

import torch
from torch.utils.data import DataLoader

from nn.dataset import AmongUsDataset
from nn.model import Model
from util.loader import read_all_recordings


def main():
    model = Model()

    # should be cross binary entropy loss for binary/bool outputs TODO: BCEWITHLOGITSLOSS
    criterion = torch.nn.MSELoss()
    optimizer = torch.optim.Adam(model.parameters(), lr=0.001)

    game_data = read_all_recordings()
    game_data.shuffle()
    train_data, val_data = game_data.split(0.8)

    train_dataset = AmongUsDataset(train_data)
    val_dataset = AmongUsDataset(val_data)

    train_loader = DataLoader(train_dataset, batch_size=32, shuffle=True)
    val_loader = DataLoader(val_dataset, batch_size=32, shuffle=True)

    for epoch in range(50):
        # train
        model.train()
        for x, y in train_loader:
            optimizer.zero_grad()
            y_pred = model(x)
            loss = criterion(y_pred, y)
            loss.backward()
            optimizer.step()

        # validate
        model.eval()
        for x, y in val_loader:
            y_pred = model(x)
            loss = criterion(y_pred, y)
            print(loss)

    torch.save(model.state_dict(), os.path.dirname(__file__) + "/model.pt")


if __name__ == "__main__":
    main()
